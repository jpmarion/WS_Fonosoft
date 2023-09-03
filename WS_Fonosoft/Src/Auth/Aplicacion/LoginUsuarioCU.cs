﻿using CifradoPE.Infraestructura.Interface;
using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Especificaciones;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class LoginUsuarioCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorio _mysqlRepositorio;
        private readonly IAes _aes;
        private readonly IUsuario _usuario;
        private readonly ValidarUsuarioNombreUsuario _validarUsuarioNombreUsuario;
        private readonly ValidarUsuarioPassword _validarUsuarioPassword = new ValidarUsuarioPassword();

        public LoginUsuarioCU(IResponse<T> response, IMysqlRepositorio mysqlRepositorio,IAes aes, IUsuario usuario) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _aes = aes;
            _usuario = usuario;
            _validarUsuarioNombreUsuario = new ValidarUsuarioNombreUsuario(_mysqlRepositorio);
            _validarUsuarioNombreUsuario.ProximaValidcion(_validarUsuarioPassword);
        }
        public override IList<T> Proceso()
        {
            _validarUsuarioNombreUsuario.EsValido(_usuario);
            _usuario.NombreUsuario = _aes.Encriptar(_usuario.NombreUsuario.ToLower());
            _usuario.Password = _aes.Encriptar(_usuario.Password);

            IUsuario usuario = _mysqlRepositorio.BuscarUsuarioXNombreUsuarioXPassword(_usuario.NombreUsuario, _usuario.Password);

            if (usuario != null)
            {
                IList<IUsuario> lstUsuario = new List<IUsuario>
                {
                    usuario
                };
                return (IList<T>)lstUsuario;
            }
            return null;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
