Public Class Frm_Credencials
    Private _Contact As DTOContact
    Private _Rol As DTORol

    Public Sub New(oContact As DTOContact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        _Rol = New DTORol(BLL.BLLSession.Current.User.Rol.Id)
        Refresca()
    End Sub

    Private Sub Refresca()
        Dim oCredencials As List(Of DTOCredencial) = BLL.BLLCredencials.All(_Rol, _Contact)
        Xl_Credencials1.Load(oCredencials)
    End Sub

    Private Sub Xl_Credencials1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Credencials1.RequestToAddNew
        Dim oCredencial As DTOCredencial = BLL.BLLCredencial.NewFromContact(_Contact)
        oCredencial.Rols.Add(_Rol)
        Dim oFrm As New Frm_Credencial(oCredencial)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Credencials1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Credencials1.RequestToRefresh
        Refresca()
    End Sub
End Class