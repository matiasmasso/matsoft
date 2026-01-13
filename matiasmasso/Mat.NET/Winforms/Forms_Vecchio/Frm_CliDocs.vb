Public Class Frm_CliDocs
    Private _Contact As DTOContact

    Public Sub New(oContact As DTOContact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_CliDocs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_CliDocs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliDocs1.RequestToAddNew
        Dim oContactDoc As New DTOContactDoc()
        With oContactDoc
            .Contact = _Contact
            .Fch = Today
        End With
        Dim oFrm As New Frm_ContactDoc(oContactDoc)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CliDocs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliDocs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oCliDocs = Await FEB2.ContactDocs.All(exs, _Contact)
        If exs.Count = 0 Then
        Else
            UIHelper.WarnError(exs)
        End If
        Xl_CliDocs1.Load(oCliDocs)
    End Function

End Class