Public Class Frm_XecsEnCartera
    Private _Banc As Banc

    Public Sub New(oBanc As Banc)
        InitializeComponent()
        _Banc = oBanc
        Me.Text = _Banc.Abr & "-Presentacio de efectes en cartera"
        Xl_XecsEnCartera1.DataSource = XecLoader.All(_Banc.Emp, Xec.StatusCods.EnCartera)
        LoadCodisPresentacio()
    End Sub

    Private Sub LoadCodisPresentacio()
        With ComboBoxCodPresentacio
            .Items.Add(New ListItem(0, "(seleccionar una modalitat)"))
            .Items.Add(New ListItem(XecsPresentacio.Modalitats.A_la_vista, "ingres a la vista"))
            .Items.Add(New ListItem(XecsPresentacio.Modalitats.Al_Cobro, "presentació al cobro"))
            .Items.Add(New ListItem(XecsPresentacio.Modalitats.Al_Descompte, "presentació al descompte"))
        End With
    End Sub

    Private Function CurrentXecs() As Xecs
        Dim retval As Xecs = Xl_XecsEnCartera1.SelectedValues
        Return retval
    End Function

    Private Sub Xl_XecsEnCartera1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_XecsEnCartera1.ItemCheck
        Dim oXecs As Xecs = CurrentXecs()
        Xl_AmtCur1.Amt = oXecs.Suma
        ButtonOk.Enabled = (oXecs.Count > 0)
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

        If CodPresentacio() = XecsPresentacio.Modalitats.NotSet Then
            MsgBox("cal triar una modalitat de presentació", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf CodPresentacio() = XecsPresentacio.Modalitats.Al_Cobro Then
            MsgBox("Ojo cal repasar com graba Cca" & vbCrLf & "abonem el compte 43100 amb càrrec al 43120 (efectes al cobro) del client")
        Else
        End If

        Dim oXecsPresentacio As New XecsPresentacio()
        With oXecsPresentacio
            .Fch = DateTimePicker1.Value
            .Banc = _Banc
            .Modalitat = CodPresentacio()
            .Xecs = CurrentXecs()
        End With

        Dim exs as New List(Of exception)
        If oXecsPresentacio.Update( exs) Then
            UIHelper.ShowStream(oXecsPresentacio.DocFile, , False)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al registrar la presentació")
        End If

    End Sub

    Private Function CodPresentacio() As XecsPresentacio.Modalitats
        Dim retval As XecsPresentacio.Modalitats = XecsPresentacio.Modalitats.NotSet
        Dim oItem As ListItem = ComboBoxCodPresentacio.SelectedItem
        If oItem IsNot Nothing Then
            retval = oItem.Value
        End If
        Return retval
    End Function
End Class