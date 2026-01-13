Public Class JsonController
    Inherits _MatController
    Public Enum Operacions
        NotSet
        Cataleg
    End Enum

    <RequireHttps>
    Public Async Function Index(op As Operacions, customer As Guid, user As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Dim oUser = Await FEB2.User.Find(user, exs)
        Select Case op
            Case Operacions.Cataleg
                Dim oCustomer As New DTOCustomer(customer)
                Dim oUserCustomers = Await FEB2.User.Contacts(exs, oUser)
                If oUserCustomers.Exists(Function(x) x.Equals(oCustomer)) Then
                    Dim oTarifa = Await FEB2.CustomerTarifa.Load(exs, oCustomer)
                    Dim oCatalog As DTOCustomerCataleg = FEB2.CustomerTarifa.CustomerCataleg(oTarifa)
                    Dim myData As Object = New With {.Catalog = oCatalog}
                    retval = Json(myData, JsonRequestBehavior.AllowGet)
                End If
        End Select
        Return retval
    End Function

End Class
