Public Class Frm_AeatMod347Item
    Private _Exercici As DTOExercici
    Private _Value As DTOAeatMod347Item

    Public Sub New(oItem As DTOAeatMod347Item, oExercici As DTOExercici)
        MyBase.New()
        Me.InitializeComponent()
        _Value = oItem
        _Exercici = oExercici
    End Sub

    Private Async Sub Frm_AeatMod347Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Mod 347 " & _Value.Contact.Nom
        TabPageCompres.Visible = Await LoadCompres()
        TabPageVendes.Visible = Await LoadVendes()
    End Sub

    Private Async Function LoadCompres() As Task(Of Boolean)
        Dim retval As Boolean
        Dim exs As New List(Of Exception)
        Dim values = Await FEB2.AeatMod347.CompresDetall(exs, _Exercici, _Value.Contact)
        If exs.Count = 0 Then
            Xl_AeatMod347CcasCompras.Load(values)
            retval = values.Count > 0
        Else
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function
    Private Async Function LoadVendes() As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim values = Await FEB2.AeatMod347.VendesDetall(exs, _Exercici, _Value.Contact)
        Xl_AeatMod347CcasCompras.Load(values)
        Return values.Count > 0
    End Function
End Class