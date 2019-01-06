using Braspag.Domain.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Braspag.Domain.Entities
{
    public class Usuario
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; }

        public string login { get; private set; }

        public string senha { get; private set; }


        protected Usuario()
        {

        }

        private Usuario(UsuarioDto dto)
        {
            this._id = dto._id;

            this.login = dto.login;

            this.senha = dto.senha;
        }

        public Usuario RetornaUsuario(UsuarioDto dto)
        {
            return new Usuario(dto);
        }

       
    }
}
