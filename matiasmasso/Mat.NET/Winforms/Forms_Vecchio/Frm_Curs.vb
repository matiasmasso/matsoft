Public Class Frm_Curs
    Private _SelectionMode As bll.dEFAULTS.SelectionModes
    Private _DefaultItem As Cur

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oSelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse, Optional oDefaultItem As Cur = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultItem = oDefaultItem
    End Sub

    Private Sub Frm_Curs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_Curs1.Load(CursLoader.All, _DefaultItem)
    End Sub

    Private Sub Xl_Curs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Curs1.onItemSelected
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_Cur(e.Argument)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()

            Case bll.dEFAULTS.SelectionModes.Selection
                RaiseEvent onItemSelected(Me, e)
                Me.Close()
        End Select
    End Sub

    Private Sub RefreshRequest()

    End Sub


End Class