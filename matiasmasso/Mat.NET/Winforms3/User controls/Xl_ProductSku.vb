Public Class Xl_ProductSku
    Inherits TextBox

    Private _Sku As DTOProductSku

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oSku As DTOProductSku)
        If oSku Is Nothing Then
            MyBase.Clear()
        Else
            MyBase.Text = oSku.nomLlarg.Tradueix(Current.Session.Lang)
        End If
    End Sub

    Private Async Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim exs As New List(Of Exception)
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Return
                If IsSameProduct() Then
                    ShowForm()
                Else
                    Await SearchSku()
                End If
        End Select
    End Sub

    Private Function IsSameProduct() As Boolean
        Dim retval = _Sku IsNot Nothing AndAlso MyBase.Text = _Sku.nomLlarg.Tradueix(Current.Session.Lang)
        Return retval
    End Function

    Private Sub ShowForm()
        Dim oFrm As New Frm_ProductSku(_Sku)
        oFrm.Show()
    End Sub

    Private Async Function SearchSku() As Task
        Dim exs As New List(Of Exception)
        Dim sKey = MyBase.Text
        If sKey = "" Then
            _Sku = Nothing
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Sku))
        ElseIf sKey.StartsWith("('") And sKey.EndsWith("' desconegut)") Then
            _Sku = Nothing
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Sku))
        Else
            Dim oMgz As DTOMgz = GlobalVariables.Emp.Mgz
            _Sku = Await Finder.FindSku(exs, Current.Session.Emp, sKey, oMgz)
            If exs.Count = 0 Then
                If _Sku Is Nothing Then
                    MyBase.Text = String.Format("('{0}' desconegut)", sKey)
                Else
                    MyBase.Text = _Sku.nomLlarg.Tradueix(Current.Session.Lang)
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Sku))
                End If
            Else
                UIHelper.WarnError(exs)
            End If

        End If

    End Function
End Class
