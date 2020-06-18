using System;
using System.Threading.Tasks;
using article_manager.Models;
using article_manager.Services;
using Microsoft.AspNetCore.Components;

namespace article_manager.Pages
{
    public class ArticleCategoriesBase : ComponentBase
    {
        protected ArticleCategoryListItem[] articleCategoryListItems = new ArticleCategoryListItem[0];
        protected ArticleCategoryItem currentCategory;
        protected string error;

        [Inject]
        private ICRUDService<ArticleCategoryListItem, ArticleCategoryItem> service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ShowList();
        }

        public async Task ShowList()
        {
            articleCategoryListItems = await service.GetList(); ;
            this.currentCategory = null;
        }

        public async Task AddCategory()
        {
            this.currentCategory = await service.GetNew();
        }

        public async Task EditCategory(ArticleCategoryListItem item)
        {
            this.currentCategory = await service.Get(item.Id);
        }

        public async Task SaveCategory(ArticleCategoryItem item)
        {
            try
            {
                if (currentCategory.Id == 0)
                {
                    await service.Create(currentCategory);
                }
                else
                {
                    await service.Update(currentCategory);
                }
                await this.ShowList();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
        }

        public async Task DeleteCategory(ArticleCategoryListItem item)
        {
            try
            {
                await service.Delete(item.Id);
                await this.ShowList();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
        }
    }
}