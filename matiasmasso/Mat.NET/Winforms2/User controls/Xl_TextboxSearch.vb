Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Xl_TextboxSearch

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Shadows Event Validated(sender As Object, e As EventArgs)

    Public ReadOnly Property Value As String
        Get
            Return TextBox1.Text
        End Get
    End Property

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Raise()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Raise()
    End Sub

    Private Sub Raise()
        Dim src As String = TextBox1.Text
        RaiseEvent AfterUpdate(Me, New MatEventArgs(src))
    End Sub

    Private Sub TextBox1_Validated(sender As Object, e As EventArgs) Handles TextBox1.Validated
        RaiseEvent Validated(Me, e)
    End Sub

    Public Function IsContainedIn(containerText As String) As Boolean
        Dim retval As Boolean
        Dim container = RemoveDiacritics(containerText)
        Dim contained = RemoveDiacritics(TextBox1.Text)
        If String.IsNullOrEmpty(contained) Then
            retval = True
        Else
            retval = Regex.IsMatch(container, Regex.Escape(contained), RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        End If
        Return retval
    End Function

    Public Function IsContainedInAny(containerTexts As IEnumerable(Of String)) As Boolean
        Dim retval As Boolean
        Dim containers = containerTexts.Select(Function(x) RemoveDiacritics(x)).ToList()
        Dim contained = RemoveDiacritics(TextBox1.Text)
        If String.IsNullOrEmpty(contained) Then
            retval = True
        Else
            For Each container As String In containers
                If Regex.IsMatch(container, Regex.Escape(contained), RegexOptions.IgnoreCase) Then
                    retval = True
                    Exit For
                End If
            Next
        End If
        Return retval
    End Function

    Public Function RemoveDiacritics(ByVal s As String) As String
        Dim normalizedString As String
        Dim stringBuilder As New StringBuilder
        normalizedString = s.Normalize(NormalizationForm.FormD)
        Dim i As Integer
        Dim c As Char
        For i = 0 To normalizedString.Length - 1
            c = normalizedString(i)
            If CharUnicodeInfo.GetUnicodeCategory(c) <> UnicodeCategory.NonSpacingMark Then
                stringBuilder.Append(c)
            End If
        Next
        Return stringBuilder.ToString()
    End Function
End Class
