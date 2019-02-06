using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
{
    [Table("text")]
    public class Text
    {
        [Key]
        [Column("text_id")]
        public int Id { get; set; }
        
        [Column("text_content")]
        public string TextContent { get; set; }
        
    }
}