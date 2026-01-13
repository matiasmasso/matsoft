

Public Class Frm_Csa_Despeses
    Private mCsa As Csa
    Private mAllowEvents As Boolean

    Public WriteOnly Property Csa() As Csa
        Set(ByVal value As Csa)
            If value IsNot Nothing Then
                mCsa = value
                PictureBoxIBAN.Image = BLL.BLLIban.Img(mCsa.Banc.Iban, BLLSession.Current.Lang)
                TextBoxCsa.Text = mCsa.ToString(BLL.BLLApp.Lang)
                TextBoxCondicions.Text = mCsa.Banc.Norma58Tarifa
                DateTimePicker1.Value = Today
                Dim Zero As DTOAmt = BLLApp.EmptyAmt
                Xl_AmtInts.Amt = Zero
                Xl_AmtCom.Amt = Zero
                Xl_AmtCorreo.Amt = Zero
                Xl_AmtIVA.Amt = Zero
                Xl_AmtCurTotal.Amt = Zero
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oInteresos As DTOAmt = Xl_AmtInts.Amt
        Dim oComisions As DTOAmt = Xl_AmtCom.Amt
        Dim oCorreo As DTOAmt = Xl_AmtCorreo.Amt

        Dim oGastos As DTOAmt = BLLApp.EmptyAmt
        oGastos.Add(Xl_AmtCom.Amt)
        oGastos.Add(Xl_AmtCorreo.Amt)

        Dim oCost As DTOAmt = oGastos.Clone
        oCost.Add(oGastos)
        Dim oIva As DTOAmt = Xl_AmtIVA.Amt
        Dim oLiquid As DTOAmt = Xl_AmtCurTotal.Amt
        Dim sFra As String = TextBoxFra.Text

        With mCsa
            .Despeses = oCost
            .Condicions = TextBoxCondicions.Text

            Dim exs as New List(Of exception)
            If .SaveDespeses_AEB58(DateTimePicker1.Value, oInteresos, oComisions, oCorreo, oIva, oLiquid, sFra, Xl_DocFile1.Value, exs) Then
                Me.Close()
            Else
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If

        End With

    End Sub


    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Xl_AmtInts.AfterUpdate, Xl_AmtCom.AfterUpdate, Xl_AmtIVA.AfterUpdate, Xl_AmtCorreo.AfterUpdate
        If mAllowEvents Then
            Calcula()
            SetDirty()
        End If

    End Sub

    Private Sub Calcula()
        Dim oSum As DTOAmt = BLLApp.EmptyAmt
        oSum.Add(Xl_AmtInts.Amt)
        oSum.Add(Xl_AmtCom.Amt)
        oSum.Add(Xl_AmtIVA.Amt)
        oSum.Add(Xl_AmtCorreo.Amt)
        Xl_AmtCurTotal.Amt = oSum
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub
End Class