Public Class Xl_LangSet
    Dim _src As DTOLang.Set
    Dim _allowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Shadows Sub Load(src As DTOLang.Set)
        _src = src
        If _src IsNot Nothing Then
            CheckBoxEsp.Checked = _src.HasLang(DTOLang.ESP())
            CheckBoxCat.Checked = _src.HasLang(DTOLang.CAT())
            CheckBoxEng.Checked = _src.HasLang(DTOLang.ENG())
            CheckBoxPor.Checked = _src.HasLang(DTOLang.POR())
            _allowEvents = True
        End If
    End Sub

    Public Function Value() As DTOLang.Set
        _src = New DTOLang.Set(CheckBoxEsp.Checked, CheckBoxCat.Checked, CheckBoxEng.Checked, CheckBoxPor.Checked)
        Return _src
    End Function

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles CheckBoxEsp.CheckedChanged,
            CheckBoxCat.CheckedChanged,
             CheckBoxEng.CheckedChanged,
              CheckBoxPor.CheckedChanged

        If _allowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Value))
        End If
    End Sub
End Class
