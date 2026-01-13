Public Class Frm_CliDocs
    Private _Contact As Contact

    Public Sub New(oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
    End Sub

    Private Sub Frm_CliDocs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_CliDocs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliDocs1.RequestToAddNew
        Dim oCliDoc As New CliDoc(_Contact)
        Dim oFrm As New Frm_CliDoc(oCliDoc)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_CliDocs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliDocs1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oCliDocs As CliDocs = CliDocsLoader.FromContact(_Contact)
        Xl_CliDocs1.Load(oCliDocs)
    End Sub

End Class