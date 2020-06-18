using System;
using System.Threading.Tasks;
using frontend.Services;
using Microsoft.AspNetCore.Components;
using shared.Models;

namespace frontend.Pages
{
    public partial class Articles : ComponentBase
    {  
        private ArticleListItem[] articleListItems = new ArticleListItem[0];
        private ArticleItem currentArticle;
        private string error; 

        [Inject]
        private ICRUDService<ArticleListItem, ArticleItem> service { get; set; }
    
        protected override async Task OnInitializedAsync()
        {
            await ShowList();
        }

        public async Task ShowList()
        {
            articleListItems = await service.GetList();
            this.currentArticle = null;
        }

        public async Task AddArticle() 
        {
            this.currentArticle = await service.GetNew();
        }

        public async Task EditArticle(ArticleListItem item) 
        {
            this.currentArticle = await service.Get(item.Id);
        }

        public async Task SaveArticle(ArticleItem item)
        {
            try
            {
                if(currentArticle.Id == 0) 
                {
                    await service.Create(currentArticle);
                } 
                else
                {
                    await service.Update(currentArticle);
                }
                await this.ShowList();
            }
            catch(Exception ex)
            {
                this.error = ex.Message;
            }
        }

        public async Task DeleteArticle(ArticleListItem item)
        {
            try
            {
                await service.Delete(item.Id);
                await this.ShowList();
            }
            catch(Exception ex)
            {
                this.error = ex.Message;
            }
        }
    }
}
