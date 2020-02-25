using AutoMapper;
using EkspertBooker.WebAPI.Database;
using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkspertBooker.Model.Requests;
using Microsoft.EntityFrameworkCore;
using EkspertBooker.WebAPI.Exceptions;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace EkspertBooker.WebAPI.Service
{
    public class KorisniciService : BaseCRUDService<Model.Korisnik, KorisniciSearchRequest, KorisnikUpsertRequest, KorisnikUpsertRequest, Database.Korisnik>, IKorisniciService
    {

        public KorisniciService(EkspertBookerContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Korisnik> Get(KorisniciSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                var query = _context.Korisnici.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request?.Ime))
                {
                    query = query.Where(k => k.Ime.Contains(request.Ime));
                }

                if (!string.IsNullOrWhiteSpace(request?.Prezime))
                {
                    query = query.Where(k => k.Prezime.Contains(request.Prezime));
                }

                List<Database.Korisnik> list = query.Include(k => k.KorisnikUloge).ToList();

                List<Database.Korisnik> filtered_list = new List<Database.Korisnik>();

                if (request.Administratori)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        foreach (Database.KorisnikUloga uloga in list[i].KorisnikUloge)
                        {
                            if (uloga.UlogaId == 1)
                            {
                                filtered_list.Add(list[i]);
                                break;
                            }
                        }
                    }
                }

                if (request.Poslodavci)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        foreach (Database.KorisnikUloga uloga in list[i].KorisnikUloge)
                        {
                            if (uloga.UlogaId == 2)
                            {
                                filtered_list.Add(list[i]);
                                break;
                            }
                        }
                    }
                }

                if (request.Eksperti)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        foreach (Database.KorisnikUloga uloga in list[i].KorisnikUloge)
                        {
                            if (uloga.UlogaId == 3)
                            {
                                filtered_list.Add(list[i]);
                                break;
                            }
                        }
                    }
                }
                if (request.Administratori || request.Eksperti || request.Poslodavci)
                {
                    filtered_list = filtered_list.GroupBy(fl => fl.KorisnikId).Select(i => i.First()).ToList();
                    return _mapper.Map<List<Model.Korisnik>>(filtered_list);
                }
                else
                {
                    return _mapper.Map<List<Model.Korisnik>>(list);
                }
            } else
            {
                //if search by username just return 1 user if found
                var korisnik = _mapper.Map<Model.Korisnik>(_context.Korisnici.Where(k => k.KorisnickoIme == request.Username).Include(k=>k.KorisnikUloge).SingleOrDefault());
                var list = new List<Model.Korisnik>();
                list.Add(korisnik);
                return list;
            }
        }

        public override Model.Korisnik GetById(int id)
        {
            var result = _mapper.Map<Model.Korisnik>(_context.Korisnici.Where(k => k.KorisnikId == id).Include(k => k.KorisnikUloge).Include(k=>k.KorisnikSlika).SingleOrDefault());
            return result;       
        }

        public override Model.Korisnik Insert(KorisnikUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Korisnik>(request);

            if (request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwordi se ne slazu");
            }

            var lozinka_salt = GenerateSalt();
            var lozinka_hash = GenerateHash(lozinka_salt, request.Password);

            entity.LozinkaHash = lozinka_hash;
            entity.LozinkaSalt = lozinka_salt;
            entity.DatumRegistracije = DateTime.Now;
            _context.Korisnici.Add(entity);
            _context.SaveChanges();

            if (entity != null)
            {
                foreach (var uloga in request.Uloge)
                {
                    var korisnikuloga = _context.KorisniciUloge.Add(new Database.KorisnikUloga
                    {
                        KorisnikId = entity.KorisnikId,
                        UlogaId = uloga.UlogaId
                    });
                    //ako je uloga ekspert, dodati u tabelu eksperti, ako je poslodavac dodati u poslodavce
                    if (uloga.UlogaId == 2)
                    {
                        //poslodavac
                        _context.Poslodavci.Add(new Database.Poslodavac
                        {
                            BrojZavrsenihProjekata = 0,
                            Korisnik = entity,
                            KorisnikUloga = korisnikuloga.Entity,
                            KorisnikUlogaId = korisnikuloga.Entity.KorisnikUlogaId,
                        });
                    }
                    if (uloga.UlogaId == 3)
                    {
                        _context.Eksperti.Add(new Database.Ekspert
                        {
                            BrojZavrsenihProjekata = 0,
                            Korisnik = entity,
                            KorisnikUloga = korisnikuloga.Entity,
                            KorisnikUlogaId = korisnikuloga.Entity.KorisnikUlogaId,
                        });
                        //ekspert
                    }
                    _context.SaveChanges();
                }
            }


            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);


            return _mapper.Map<Model.Korisnik>(entity);
        }

        [Authorize(Roles = "Administrator")]
        public override Model.Korisnik Update(int id, KorisnikUpsertRequest request)
        {
            var entity = _context.Korisnici.Include(k=>k.KorisnikSlika).Where(k=>k.KorisnikId == id).SingleOrDefault();
            _mapper.Map(request, entity);
            _context.SaveChanges();

            if (!string.IsNullOrWhiteSpace(request.Password) && !string.IsNullOrWhiteSpace(request.PasswordConfirmation))
            {
                if (request.Password != request.PasswordConfirmation)
                {
                    throw new UserException("Passwordi se ne slažu");
                }
                //novi password
                var new_salt = GenerateSalt();
                var new_password_hash = GenerateHash(new_salt, request.Password);
                entity.LozinkaSalt = new_salt;
                entity.LozinkaHash = new_password_hash;
                _context.SaveChanges();
            }

            return _mapper.Map<Model.Korisnik>(entity);
        }

        [Authorize(Roles = "Administrator")]
        public override bool Delete(int id)
        {
            //korisnici se ne brisu 
            return false;
        }

        public Model.Korisnik Authenticate(string username, string password)
        {
            var user = _context.Korisnici.Include(u => u.KorisnikUloge).Where(u => u.KorisnickoIme == username).SingleOrDefault();
            foreach(var uloga in user.KorisnikUloge)
            {
                uloga.Uloga = _context.Uloge.Find(uloga.UlogaId);
            }

            if (user != null)
            {
                var password_hash = GenerateHash(user.LozinkaSalt, password);
                if (password_hash == user.LozinkaHash)
                {
                    //user autorizovan
                    return _mapper.Map<Model.Korisnik>(user);
                }
            }

            return null;
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

    }
}
