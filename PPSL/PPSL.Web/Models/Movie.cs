using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PPSL.Web.Models
{
  public class Movie
  {
    // DataType 特性不提供任何验证 知识为了帮助视图引擎设置数据格式
    public int Id { get; set; }


    [Display(Name = "标题")]
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string Title { get; set; }

    [Display(Name = "发布日期")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }


    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
    [Display(Name = "分类")]
    [StringLength(30)]
    public string Genre { get; set; }

    
    [Range(1,100)]
    [Display(Name = "价格")]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }


    [Display(Name = "分级")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    public string Rating { get; set; }
  }
}
