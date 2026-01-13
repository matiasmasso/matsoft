Public Class Xl_LangTextShort
    Inherits Xl_LookupTextboxButton

    Private _LangText As DTOLangText

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oLangText As DTOLangText)
        _LangText = oLangText
        Refresca()
    End Sub

    Public ReadOnly Property LangText() As DTOLangText
        Get
            Return _LangText
        End Get
    End Property

    Public Sub Clear()
        _LangText = Nothing
        Refresca()
    End Sub

    Private Sub Xl_LookupLangText_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_LangTextShort(_LangText)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupLangText_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_LangTextShort(_LangText)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub onLangTextSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _LangText = e.Argument
        RaiseEvent AfterUpdate(Me, e)
        Refresca()
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        Refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Refresca()
        If _LangText Is Nothing Then
            MyBase.Text = ""
        Else
            If _LangText.IsMultiLang Then
                MyBase.TextBox1.ReadOnly = True
                MyBase.Text = _LangText.Esp & " / " & _LangText.Cat & " / " & _LangText.Eng & " / " & _LangText.Por
            Else
                MyBase.TextBox1.ReadOnly = False
                MyBase.Text = _LangText.Esp
            End If
        End If
    End Sub
End Class
