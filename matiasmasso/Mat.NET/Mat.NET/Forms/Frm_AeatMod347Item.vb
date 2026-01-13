Public Class Frm_AeatMod347Item
    Private _Exercici As DTOExercici
    Private _Value As DTOAeatMod347Item

    Public Sub New(oItem As DTOAeatMod347Item, oExercici As DTOExercici)
        MyBase.New()
        Me.InitializeComponent()
        _Value = oItem
        _Exercici = oExercici
    End Sub

    Private Sub Frm_AeatMod347Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Mod 347 " & _Value.Contact.Nom
        Dim exs As New List(Of Exception)
        Dim values As List(Of DTOAeatMod347Cca) = BLL.BLLAeatMod347.CompresDetall(_Exercici, _Value.Contact, exs)
        Xl_AeatMod347Ccas1.Load(values)
    End Sub
End Class