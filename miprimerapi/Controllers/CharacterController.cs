using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using miprimerapi.Models;

namespace miprimerapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        public static List<Character> character = new List<Character>()
        {
            new Character()
            { 
              id = 1,
              name = "Kratos",
              game = "God Of War",
              role = "Main Character"
            },
            new Character()
            { 
                id = 2,
                name = "Lion",
                game = "Resident evil",
                role = "Main Character"
            },
            new Character()
            { 
                id = 3,
                name = "Prowler",
                game = "Spiderman",
                role = "Villain"
            },
        };


        [HttpGet]
        public IActionResult getCharacters()
        {
            return Ok(character);
        }

        [HttpGet("{id}")]
        public IActionResult getCharactersByID(int id)
        {
            
            foreach (var c in character)
            {
                if (c.id == id)
                {
                    return Ok(c);
                }
            }
            // for (int i = 0; i < character.Count; i++)
            // {
            //     if (character[i].id == id)
            //     {
            //         return Ok(character[i]);
            //     }
            // }

            return NotFound(new{message = $"no se encontro ningun personaje con id {id}"});
        }

        [HttpPost]
        public IActionResult createCharacter(Character newcharacter)
        {
            int MaxID = 0;
            if (character.Count > 0)
            {
                foreach (var c in character)
                {
                    if (c.id > MaxID)
                    {
                        MaxID = c.id;
                    }
                }
                newcharacter.id = MaxID + 1;
            }
            else
            {
                newcharacter.id = 1;
            }
            character.Add(newcharacter);
            return Ok(newcharacter);
        }

        [HttpPut("{id}")]
        public IActionResult updateCharacter(int id, Character updatedCharacter)
        {
            
            foreach (var c in character)
            {
                if (c.id == id)
                {
                    c.name = updatedCharacter.name;
                    c.game = updatedCharacter.game;
                    c.role = updatedCharacter.role;
                    
                    return Ok(c);
                } 
                
            }

            return NotFound(new {Message = $"no se encontro el personaje con id {id}"});

        }

        [HttpDelete("{id}")]
        public IActionResult deleteCharacter(int id)
        {
            Character characterToDelete = null;
            foreach (var c in character)
            {
                if (c.id == id)
                {
                    characterToDelete = c;
                    break;
                }
                
            }

            character.Remove(characterToDelete);
            return Ok(new { message = $"Personaje borrado con exito" });
        }
    }
}
