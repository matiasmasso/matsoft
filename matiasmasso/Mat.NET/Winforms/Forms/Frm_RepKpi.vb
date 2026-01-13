Public Class Frm_RepKpi

    Private _RepKpi As DTORepKpi
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTORepKpi)
        MyBase.New()
        Me.InitializeComponent()
        _RepKpi = value
        BLL.BLLRepKpi.Load(_RepKpi)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _RepKpi
            TextBoxEsp.Text = .Nom.Esp
            TextBoxCat.Text = .Nom.Cat
            TextBoxEng.Text = .Nom.Eng
            TextBoxPor.Text = .Nom.Por
            ButtonOk.Enabled = .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           TextBoxPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _RepKpi
            .Nom = New DTOLangText(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text, TextBoxPor.Text)
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLRepKpi.Update(_RepKpi, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepKpi))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class

