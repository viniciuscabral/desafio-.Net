using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WsProjetoVinicius.Model;
using WsTestes.Model;
using WsTestes.Repo;

namespace WsProjetoVinicius.Controllers
{
    public class UserServices : IUserService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; ;
        }

        public UserDto AddUser(User user)
        {
            //Verifica se chegou alguma informação a ser inserida
            if (user != null)
            {
                //Verifica se existe algum campo inválido(não preenchido)
                if (!String.IsNullOrEmpty(user.Email) && !String.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.LastName)
                    && !String.IsNullOrEmpty(user.Password) && user.Phones != null)
                {
                    //Verifica se email já está cadastrado
                    if (_context.User.Any(o => o.Email == user.Email))
                    {
                        throw new DuplicateUserException("E-mail already exists"); 
                    }
                    else
                    {
                        _context.User.Add(user);
                        _context.SaveChanges();
                        UserDto userDto = _mapper.Map<UserDto>(user);
                        return userDto; // se tudo ok
                    }
                }
                else
                {
                    throw new ArgumentException("Missing fields"); // se campos vazios
                }
            }
            else
            {
                throw new ArgumentNullException("Invalid fields"); // se nada enviado
            }
        }

        public UserDto SingIn(User user)
        {
            //Verifica se existe algum campo inválido(não preenchido)
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Missing fields");
            }
            else
            {
                User userRet = _context.User.Include(d => d.Phones).FirstOrDefault(o => o.Email == user.Email
                                            && o.Password == user.Password);
                if (userRet != null)
                {
                    userRet.Last_login = DateTime.Now.ToString();
                    _context.User.Update(userRet);
                    _context.SaveChanges();

                    UserDto userDto = _mapper.Map<UserDto>(userRet);
                    return userDto;
                }
                else
                {
                    throw new NotFoundException("Invalid e-mail or password");
                }
            }
        }

        public UserDto AutorizationUser(string autentication)
        {
            if(autentication == null || autentication == "")
                throw new UnauthorizedException("Unauthorized");

            User userRet = _context.User.Include(d => d.Phones).FirstOrDefault(i => i.Autentication == autentication);
            if (userRet == null)
            {
                throw new UnauthorizedException("Unauthorized - invalid session");
            }
            else
            {                
                UserDto userDto = _mapper.Map<UserDto>(userRet);
                return userDto; 
            }
        }

        public IEnumerable<User> GetAllUser() // utilizado apenas para facilitar a amostra com o autentication
        {
           List<User> users = _context.User.Include(d => d.Phones).ToList();
            if (users.Count<User>() > 0)
                return _context.User;
            else
                throw new NotFoundException("Not found object");
        }

    }
}
