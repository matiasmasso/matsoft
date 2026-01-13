Public Class Frm_Cods
    Private _Root As DTOCod
    Private _DefaultValue As DTOCod
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oRoot As DTOCod = Nothing, Optional oDefaultValue As DTOCod = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _Root = oRoot
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Cods_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Root IsNot Nothing Then
            Me.Text = _Root.Nom.Tradueix(Current.Session.Lang)
        End If
        Await refresca()
    End Sub

    Private Sub Xl_Cods1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Cods1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Cods1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Cods1.RequestToAddNew
        Dim oCod = DTOCod.Factory(Current.Session.User, _Root)
        Dim oFrm As New Frm_Cod(oCod)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Cods1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Cods1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oCods = Await FEB2.Cods.All(exs, _Root)

        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Cods1.Load(oCods, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class