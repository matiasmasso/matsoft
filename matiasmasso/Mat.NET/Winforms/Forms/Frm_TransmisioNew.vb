Public Class Frm_TransmisioNew
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Async Sub Frm_TransmisioNew_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB2.Deliveries.PendentsDeTransmetre(GlobalVariables.Emp.Mgz, exs)
        If exs.Count = 0 Then
            Xl_Transmisio_SelectAlbs1.Load(oDeliveries)
            TextBoxEmail.Text = Await FEB2.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.EmailTransmisioVivace, exs)
            If exs.Count = 0 Then
                SetStatus()
                SetMenu()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oTransmisio As DTOTransmisio = GetTransmisio()
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Transmisio.Update(oTransmisio, exs) Then
            If CheckBoxEmail.Checked Then
                If Await FEB2.Transmisio.Send(exs, oTransmisio, TextBoxEmail.Text) Then
                    UIHelper.ToggleProggressBar(Panel1, False)
                    MsgBox("Transmisió enviada correctament", MsgBoxStyle.Information)
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(oTransmisio))
                    Me.Close()
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                MsgBox("Transmisio generada correctament." & vbCrLf & "No ha estat enviada", MsgBoxStyle.Information)
                RaiseEvent AfterUpdate(Me, New MatEventArgs(oTransmisio))
                Me.Close()
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function GetTransmisio() As DTOTransmisio
        Dim exs As New List(Of Exception)
        Dim retval = DTOTransmisio.Factory(Current.Session.Emp, GlobalVariables.Emp.Mgz, Xl_Transmisio_SelectAlbs1.CheckedValues)
        For Each oDelivery As DTODelivery In retval.Deliveries
            FEB2.Delivery.Load(oDelivery, exs)
        Next
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function

    Private Sub SetMenu()
        ArxiuToolStripMenuItem.DropDownItems.Add("Selecciona tot", Nothing, AddressOf Do_SelectAll)
        ArxiuToolStripMenuItem.DropDownItems.Add("Selecciona res", Nothing, AddressOf Do_SelectNone)
        ArxiuToolStripMenuItem.DropDownItems.Add("Deselecciona la resta", Nothing, AddressOf Do_DeSelectRest)
    End Sub

    Private Sub Do_SelectAll()
        Xl_Transmisio_SelectAlbs1.SelectAllItems()
    End Sub

    Private Sub Do_SelectNone()
        Xl_Transmisio_SelectAlbs1.SelectNoItems()
    End Sub

    Private Sub Do_DeSelectRest()
        Xl_Transmisio_SelectAlbs1.DeSelectRest()
    End Sub


    Private Sub CheckBoxEmail_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEmail.CheckedChanged
        If _AllowEvents Then
            TextBoxEmail.Visible = CheckBoxEmail.Checked
        End If
    End Sub

    Private Sub Xl_Transmisio_SelectAlbs1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Xl_Transmisio_SelectAlbs1.ItemCheck
        SetStatus()
    End Sub

    Private Sub SetStatus()
        Dim items As List(Of DTODelivery) = Xl_Transmisio_SelectAlbs1.CheckedValues
        If items.Count = 0 Then
            TextBoxStatus.Clear()
            ButtonOk.Enabled = False
        Else

            TextBoxStatus.Text = String.Format("{0} albarans per {1}", items.Count, DTOAmt.CurFormatted(FEB2.Deliveries.Sum(items)))
            ButtonOk.Enabled = True
        End If
    End Sub
End Class