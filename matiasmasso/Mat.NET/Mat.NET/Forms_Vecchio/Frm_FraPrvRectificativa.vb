

Public Class Frm_FraPrvRectificativa
    Private mFra As FacturaDeProveidor = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oFacturaOriginal As FacturaDeProveidor)
        MyBase.New()
        Me.InitializeComponent()
        mFra = oFacturaOriginal
        Xl_Contact_Logo1.Contact = mFra.Proveidor
        TextBoxFraOriginal.Text = GetFraOriginalText()
    End Sub

    Private Function GetFraOriginalText()
        Dim sB As New System.Text.StringBuilder
        sB.AppendLine("fra. " & mFra.Num & " del " & mFra.Fch.ToShortDateString)
        sB.AppendLine("base " & mFra.Bas.Formatted & " + IVA " & mFra.Iva.CurFormat & " = " & mFra.Liq.Formatted & " Vto." & mFra.Vto.ToShortDateString)
        If mFra.PendentDeAbonar.Eur <> 0 Then
            sB.AppendLine("import pendent de regularitzar: " & mFra.PendentDeAbonar.Formatted)
        End If
        sB.AppendLine(mFra.Obs)
        Return sB.ToString
    End Function


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oFra As FacturaDeProveidor = mFra.NewFacturaRectificativa(TextBoxNum.Text, DateTimePickerFch.Value)
        With oFra
            .Vto = DateTimePickerVto.Value
            .Bas = Xl_AmtBas.Amt
            .Iva = Xl_AmtIva.Amt
            .Liq = Xl_AmtTot.Amt
            .Update(Xl_DocFile1.Value, mFra)
        End With

        Dim sRectificadaPer As String = " rectificada per " & oFra.Num
        Dim oCcaOriginal As Cca = mFra.Cca
        With oCcaOriginal
            If .Txt.Length + sRectificadaPer.Length <= 60 Then
                .Txt = .Txt & sRectificadaPer
            Else
                .Txt = .Txt.Substring(0, 60 - sRectificadaPer.Length) & sRectificadaPer
            End If
            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
        Dim sql As String = "UPDATE FRAPRV SET PENDENTABONAR=" & Xl_AmtPendent.Amt.Eur.ToString.Replace(",", ".") & " WHERE GUID LIKE '" & mFra.Guid.ToString & "'"
        maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)

        RaiseEvent AfterUpdate(oFra, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNum.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_AmtBas_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtBas.AfterUpdate
        Dim oBas As maxisrvr.Amt = Xl_AmtBas.Amt.Clone
        Dim oIva As maxisrvr.Iva = maxisrvr.Iva.Standard(DateTimePickerFch.Value)
        Dim oAmtIva As maxisrvr.Amt = oIva.Quota(oBas)
        Xl_AmtIva.Amt = oAmtIva
        Dim oTot As maxisrvr.Amt = oBas.Clone
        oTot.Add(oAmtIva)
        Xl_AmtTot.Amt = oTot
    End Sub

    Private Sub Xl_AmtIva_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtIva.AfterUpdate
        Dim oTot As maxisrvr.Amt = Xl_AmtBas.Amt.Clone
        Dim oIva As maxisrvr.Amt = Xl_AmtIva.Amt.Clone
        oTot.Add(oIva)
        Xl_AmtTot.Amt = oTot
    End Sub
End Class