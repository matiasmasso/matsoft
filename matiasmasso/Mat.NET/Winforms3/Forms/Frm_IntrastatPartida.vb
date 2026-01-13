Public Class Frm_IntrastatPartida
    Private _IntrastatPartida As DTOIntrastat.Partida

    Public Sub New(value As DTOIntrastat.Partida)
        MyBase.New
        InitializeComponent()
        _IntrastatPartida = value
    End Sub

    Private Sub Frm_IntrastatPartida_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub


    Private Sub refresca()
        With _IntrastatPartida
            TextBoxCodiMercancia.Text = DTOCodiMercancia.FullNom(.CodiMercancia)
        End With
    End Sub

    Private Sub TextBoxCodiMercancia_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCodiMercancia.TextChanged
        Dim oFrm As New Frm_CodiMercancia(_IntrastatPartida.CodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

End Class