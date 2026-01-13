@ModelType DescatalogadosModel



    @For Each oColumn In Model.Sheet.columns
        @<div>
            @oColumn.header
        </div>
    Next


    @For Each oRow In Model.Sheet.rows
        For Each oCell In oRow.cells
            @<a href="@oCell.url" target="_blank">
                @oCell.content
            </a>
        Next
    Next




