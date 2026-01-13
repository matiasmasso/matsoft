Public Class Frm_Noticias2
    Private _DefaultValue As DTONoticia
    Private _Src As DTONoticiaBase.Srcs
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTONoticia = Nothing, Optional oSrc As DTONoticiaBase.Srcs = DTONoticiaBase.Srcs.News, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _Src = oSrc
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Noticias_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Noticias1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Noticias21.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Noticias1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Noticias21.RequestToAddNew
        Dim oNoticia As New DTONoticia()
        Dim oFrm As New Frm_Noticia(oNoticia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Noticias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Noticias21.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oNoticias As List(Of DTONoticia) = BLL.BLLNoticias.Headers(_Src)
        Xl_Noticias21.Load(oNoticias, DTONoticiaBase.Srcs.News, _SelectionMode)
    End Sub

End Class