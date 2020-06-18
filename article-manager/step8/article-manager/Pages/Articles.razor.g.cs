using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using article_manager.Models;
using article_manager.Services;
using frontendlib.Models;

namespace article_manager.Pages
{
    public partial class Articles : ComponentBase
    {  
        private ItemListModel articlesModel = new ItemListModel()
        {
            ItemName = "Article",
            Headers =  new string[] { "Id", "Title", "Category"},
            Items = new ArticleListItem[0]
        };

        protected ItemDetailsModel<ArticleItem> articleModel = new ItemDetailsModel<ArticleItem>()
        {
            ItemName = "Category"
        };

        private string error; 

        [Inject]
        private ICRUDService<ArticleListItem, ArticleItem> service { get; set; }
    
        protected override async Task OnInitializedAsync()
        {
            await ShowList();
        }

        public async Task ShowList()
        {
            articlesModel.Items = await service.GetList();
            articleModel.Item = null;
        }

        public async Task AddArticle() 
        {
            this.articleModel.Item = await service.GetNew();
        }

        public async Task EditArticle(object item) 
        {
            this.articleModel.Item = await service.Get(((ArticleListItem)item).Id);
        }

        public async Task SaveArticle(ArticleItem item)
        {
            try
            {
                if(articleModel.Item.Id == 0) 
                {
                    await service.Create(articleModel.Item);
                } 
                else
                {
                    await service.Update(articleModel.Item);
                }
                await this.ShowList();
            }
            catch(Exception ex)
            {
                this.error = ex.Message;
            }
        }

        public async Task DeleteArticle(object item)
        {
            try
            {
                await service.Delete(((ArticleListItem)item).Id);
                await this.ShowList();
            }
            catch(Exception ex)
            {
                this.error = ex.Message;
            }
        }
    }
}
