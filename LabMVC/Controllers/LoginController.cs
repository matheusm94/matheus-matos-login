using LabMVC.DTO;
using LabMVC.ModelViews;
using LabMVC.Services;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace LabMVC.Controllers
{
    public class LoginController : Controller
    {
        public UsuarioService usuarioService = new UsuarioService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logar(LoginDTO loginDTO)
        {
            var usuario = usuarioService.BuscaLogin(loginDTO);
            var loginPermitido = usuarioService.VerificaSenha(usuario, loginDTO.Senha);
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Login) || string.IsNullOrEmpty(loginDTO.Senha)) 
                return View("index", new ErroModelView { Mensagem = "Login ou senha inválida" });

            if(loginPermitido)
            {
                var cookie = new HttpCookie("usuario_logado");

                string encryptedText = LabMVC.Cripto.Encript.Encrypt(JsonConvert.SerializeObject(loginDTO), "12188282sjjabqghhnnwqwqw");

                cookie.Value = encryptedText;
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                //Session["usuario_logado"] = loginDTO; 

                return Redirect("/");
            }

            return View("index", new ErroModelView { Mensagem = "Login ou senha inválida" });
        }
    }
}