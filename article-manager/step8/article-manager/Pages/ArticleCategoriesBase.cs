using System;
using System.Threading.Tasks;
using article_manager.Models;
using article_manager.Services;
using Microsoft.AspNetCore.Components;
using frontendlib.Models;

namespace article_manager.Pages
{
     public class ArticleCategoriesBase : ComponentBase
    {
        protected ItemListModel categoriesModel = new ItemListModel()
        {
            ItemName = "Category",
            Headers =  new string[] { "Id", "Name"},
            Items = new ArticleCategoryListItem[0]
        };

        protected ItemDetailsModel<ArticleCategoryItem> categoryModel = new ItemDetailsModel<ArticleCategoryItem>()
        {
            ItemName = "Category"
        };

        protected string error;

        [Inject]
        private ICRUDService<ArticleCategoryListItem, ArticleCategoryItem> service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ShowList();
        }

        public async Task ShowList()
        {
            this.categoriesModel.Items = await service.GetList(); ;
            this.categoryModel.Item = null;
        }

        public async Task AddCategory()
        {
            this.categoryModel.Item = await service.GetNew();
        }

        public async Task EditCategory(object item)
        {
            this.categoryModel.Item = await service.Get(((ArticleCategoryListItem)item).Id);
        }

        public async Task SaveCategory(ArticleCategoryItem item)
        {
            try
            {
                if (this.categoryModel.Item.Id == 0)
                {
                    await service.Create(this.categoryModel.Item);
                }
                else
                {
                    await service.Update(this.categoryModel.Item);
                }
                await this.ShowList();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
        }

        public async Task DeleteCategory(object item)
        {
            try
            {
                await service.Delete(((ArticleCategoryListItem)item).Id);
                await this.ShowList();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
        }
    }
}