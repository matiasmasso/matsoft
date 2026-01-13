Public Class Frm_Importacions2
    Private _Proveidor As Proveidor
    Private _emp as DTOEmp

    Public Sub New(Optional oProveidor As Proveidor = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = oProveidor
        If oProveidor Is Nothing Then
            _emp = BLL.BLLApp.Emp
        Else
            _Proveidor = oProveidor
            _Emp = _Proveidor.Emp
        End If
    End Sub

    Private Sub Frm_Importacions2_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Importacions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Importacions1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oImportacions As List(Of Importacio) = ImportacioLoader.FindAll(_Emp, _Proveidor)
        Xl_Importacions1.Load(oImportacions)
    End Sub

End Class
