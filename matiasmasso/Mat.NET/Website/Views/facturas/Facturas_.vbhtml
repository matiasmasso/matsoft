@ModelType DTOCustomer
@Code
    Dim exs As New List(Of Exception)
    Dim oInvoices = FEB.Invoices.PrintedSync(exs, Model)
    Dim pagesize As Integer = 15
End Code



    @If oInvoices.Count = 0 Then
        @<div>
            No nos constan facturas registradas
        </div>        
    Else
        @<div>
            <div id="Items">
                @Html.Partial("Facturas__", oInvoices.Take(pagesize))
            </div>

            <div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-guid="@Model.Guid.ToString" data-pagesize='@pagesize' data-itemscount='@oInvoices.Count'></div>
        </div>
    End If



