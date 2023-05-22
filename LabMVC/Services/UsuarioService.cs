using LabMVC.DTO;
using LabMVC.Repositories;
using LabWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LabMVC.Services
{
    public class UsuarioService
    {
        LoginService loginservice = new LoginService();
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        public Usuario BuscaLogin(LoginDTO loginDTO)
        {
            //loginDTO.Senha = loginservice.EncryptPassword(loginDTO.Senha);
            return usuarioRepository.BuscaLogin(loginDTO.Login, loginDTO.Senha);
        }

        public bool VerificaSenha(Usuario usuario, string senhaLogin)
        {
            var senhaSalva = loginservice.DecryptPassword(usuario.Senha);

            if (!senhaSalva.Equals(senhaLogin))
            {
                return false;
            }
            else { return true; }
        }

        public void Salvar(Usuario usuario)
        {
           // usuario.Senha = loginservice.EncryptPassword(usuario.Senha);
            usuarioRepository.Salvar(usuario);
        }
    }

}