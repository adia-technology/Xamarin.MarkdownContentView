using System;
using System.Linq;
using System.Threading.Tasks;
using Markdig.Extensions.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xamarin.MarkdownContentView.Rendering.Markdown.Internal.Renderers
{
    public class Table : BaseViewInnerRenderer, IBlockRenderer<Markdig.Extensions.Tables.Table>
    {
        private readonly IInnerRender _render;

        public Table(IInnerRender render, Func<string, Task> navigateToLink) : base(navigateToLink)
        {
            _render = render;
        }

        public View Render(Markdig.Extensions.Tables.Table block, MarkdownTheme theme)
        {
            var grid = new Grid();

            AddRowsAndColumnsDefinitions(block, grid);
            RenderRowsAndColumns(block, grid, theme);

            return grid;
        }

        private void RenderRowsAndColumns(Markdig.Extensions.Tables.Table block, Grid grid, MarkdownTheme theme)
        {
            for (var rowIndex = 0; rowIndex < block.Count; rowIndex++)
            {
                var rowBlock = block.ElementAt(rowIndex);

                if (!(rowBlock is TableRow row))
                {
                    continue;
                }

                for (var columnIndex = 0; columnIndex < row.Count; columnIndex++)
                {
                    var columnBlock = row.ElementAt(columnIndex);

                    if (!(columnBlock is TableCell cell))
                    {
                        continue;
                    }

                    var cellContent = cell.AsEnumerable();
                    var child = _render.Render(cellContent, theme);

                    grid.Children.Add(child, columnIndex, rowIndex);
                }
            }
        }

        private static void AddRowsAndColumnsDefinitions(Markdig.Extensions.Tables.Table block, Grid grid)
        {
            block.ColumnDefinitions.ForEach(_ =>
                grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Auto)}));

            block.ForEach(_ =>
                grid.RowDefinitions.Add((new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)})));
        }
    }
}
