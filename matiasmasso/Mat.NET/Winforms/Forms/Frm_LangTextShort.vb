Public Class Frm_LangTextShort
    Private _LangText As DTOLangText
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oLangText As DTOLangText)
        MyBase.New
        InitializeComponent()

        _LangText = oLangText
    End Sub

    Private Sub Frm_LangTextShort_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _LangText
            TextBoxEsp.Text = .Esp
            TextBoxCat.Text = .Cat
            TextBoxEng.Text = .Eng
            TextBoxPor.Text = .Por
        End With
        _AllowEvents = True
    End Sub

    Private Sub TextBoxEsp_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           TextBoxPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _LangText
            .Esp = TextBoxEsp.Text
            .Cat = TextBoxCat.Text
            .Eng = TextBoxEng.Text
            .Por = TextBoxPor.Text
        End With
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_LangText))
        Me.Close()
    End Sub
End Class