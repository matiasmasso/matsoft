@ModelType MaxiSrvr.Stat


<div class="row">
    @For Each ColumnHeader As String In Model.ColumnHeaders
        @<div>
            @ColumnHeader
        </div>
    Next
</div>

@For i As Integer = 0 To Model.Items.Count - 1
    @If Model.Items(i).Key = Model.SelectedKey Then
        @<a href="#" class='row active' data-key="@Model.Items(i).Key">
            <div>
                @Model.Items(i).Concept
            </div>
            @For j As Integer = 0 To Model.Items(i).Values.Count - 1
                @<div>
                    @Format(CDec(Model.Items(i).Values(j)), "#,###.00;-#,###.00;#")
                </div>
            Next
        </a>
    Else
        @<a href="#" class='row' data-key="@Model.Items(i).Key">
            <div>
                @Model.Items(i).Concept
            </div>
            @For j As Integer = 0 To Model.Items(i).Values.Count - 1
                @<div>
                    @Format(CDec(Model.Items(i).Values(j)), "#,###.00;-#,###.00;#")
                </div>
            Next
        </a>
    End If

Next
