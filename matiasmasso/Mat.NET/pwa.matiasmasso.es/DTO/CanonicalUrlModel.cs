using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.CatalogDTO;
using static DTO.VehicleModel;

namespace DTO
{
    public class CanonicalUrlModel
    {
        public Guid Target { get; set; }
        public LangTextDTO Url { get; set; } = new();

        public static CanonicalUrlModel Factory(Guid target
            , string? brandEsp = null
            , string? brandCat = null
            , string? brandEng = null
            , string? brandPor = null
            , string? deptEsp = null
            , string? deptCat = null
            , string? deptEng = null
            , string? deptPor = null
            , string? categoryEsp = null
            , string? categoryCat = null
            , string? categoryEng = null
            , string? categoryPor = null
            , string? skuEsp = null
            , string? skuCat = null
            , string? skuEng = null
            , string? skuPor = null
            , bool? includeDept = false
            )
        {
            var retval = new CanonicalUrlModel();
            retval.Target = target;
            if(skuEsp == null)
            {
                if(categoryEsp == null)
                {
                    if (deptEsp == null)
                    {
                        retval.Url = new LangTextDTO(brandEsp, brandCat, brandEng, brandPor);
                    } else
                    {
                        var brand = new LangTextDTO(brandEsp, brandCat, brandEng, brandPor);
                        var dept =  new LangTextDTO(deptEsp, deptCat, deptEng, deptPor);
                        retval.Url = new LangTextDTO
                        {
                            Esp = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Esp()), dept.Tradueix(LangDTO.Esp())),
                            Cat = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Cat()), dept.Tradueix(LangDTO.Cat())),
                            Eng = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Eng()), dept.Tradueix(LangDTO.Eng())),
                            Por = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Por()), dept.Tradueix(LangDTO.Por()))
                        };
                    }
                }
                else
                {
                    var brand = new LangTextDTO(brandEsp, brandCat, brandEng, brandPor);
                    var category = new LangTextDTO(categoryEsp, categoryCat, categoryEng, categoryPor);
                    if(includeDept ?? false)
                    {
                        var dept = new LangTextDTO(deptEsp, deptCat, deptEng, deptPor);
                        retval.Url = new LangTextDTO
                        {
                            Esp = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Esp()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Esp())),
                            Cat = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Cat()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Cat())),
                            Eng = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Eng()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Eng())),
                            Por = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Por()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Por()))
                        };
                    }
                    else
                    {
                        retval.Url = new LangTextDTO
                        {
                            Esp = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Esp())),
                            Cat = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Cat()), category.Tradueix(LangDTO.Cat())),
                            Eng = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Eng()), category.Tradueix(LangDTO.Eng())),
                            Por = String.Format("{0}/{1}", brand.Tradueix(LangDTO.Por()), category.Tradueix(LangDTO.Por()))
                        };
                    }
                }
            } else 
            {
                var brand = new LangTextDTO(brandEsp, brandCat, brandEng, brandPor);
                var category = new LangTextDTO(categoryEsp, categoryCat, categoryEng, categoryPor);
                var sku = new LangTextDTO(skuEsp, skuCat, skuEng, skuPor);
                if (includeDept ?? false)
                {
                    var dept = new LangTextDTO(deptEsp, deptCat, deptEng, deptPor);
                    retval.Url = new LangTextDTO
                    {
                        Esp = String.Format("{0}/{1}/{2}/{3}", brand.Tradueix(LangDTO.Esp()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Esp()), sku.Tradueix(LangDTO.Esp())),
                        Cat = String.Format("{0}/{1}/{2}/{3}", brand.Tradueix(LangDTO.Cat()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Cat()), sku.Tradueix(LangDTO.Cat())),
                        Eng = String.Format("{0}/{1}/{2}/{3}", brand.Tradueix(LangDTO.Eng()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Eng()), sku.Tradueix(LangDTO.Eng())),
                        Por = String.Format("{0}/{1}/{2}/{3}", brand.Tradueix(LangDTO.Por()), dept.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Por()), sku.Tradueix(LangDTO.Por()))
                    };
                }
                else
                {
                    retval.Url = new LangTextDTO
                    {
                        Esp = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Esp()), category.Tradueix(LangDTO.Esp()), sku.Tradueix(LangDTO.Esp())),
                        Cat = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Cat()), category.Tradueix(LangDTO.Cat()), sku.Tradueix(LangDTO.Cat())),
                        Eng = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Eng()), category.Tradueix(LangDTO.Eng()), sku.Tradueix(LangDTO.Eng())),
                        Por = String.Format("{0}/{1}/{2}", brand.Tradueix(LangDTO.Por()), category.Tradueix(LangDTO.Por()), sku.Tradueix(LangDTO.Por()))
                    };
                }
            }
            return retval;
        }

        public override string ToString() => Url.Tradueix(LangDTO.Esp());

    }
}
