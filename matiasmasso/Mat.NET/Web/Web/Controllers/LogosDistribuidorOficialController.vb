Public Class LogosDistribuidorOficialController
    Inherits _MatController

    Function Logos() As ActionResult
        Return LoginOrView("LogosDistribuidorOficial")
    End Function

    Async Function Logo(brand As Guid, customer As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oBrand = Await FEB2.ProductBrand.Find(exs, brand)
        Dim oCustomer As New DTOCustomer(customer)
        FEB2.Contact.Load(oCustomer, exs)
        Dim Model As New DTOCliProduct
        With Model
            .Product = oBrand
            .Customer = oCustomer
        End With
        Return View("LogoDistribuidorOficial", Model)
    End Function


End Class