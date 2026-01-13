Imports System.ComponentModel

Public Class Xl_Zip2
    Inherits TextBox

    Private _Zip As DTOZip

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oZip As DTOZip)
        _Zip = oZip
        If _Zip Is Nothing Then
            MyBase.Clear()
        Else
            MyBase.Text = DTOZip.FullNom(_Zip)
        End If
    End Sub

    Public ReadOnly Property Value As DTOZip
        Get
            Return _Zip
        End Get
    End Property

    Private Sub Xl_Zip2_Validating(sender As Object, e As CancelEventArgs) Handles Me.Validating
    End Sub

    Private Sub Xl_Zip2_Validated(sender As Object, e As EventArgs) Handles Me.Validated
        If MyBase.Text > "" And MyBase.Text <> _Zip.ZipCod Then

        End If
    End Sub
End Class
