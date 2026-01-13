

Public Class Frm_BookFra
    Private mBookFra As BookFra
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oBookFra As BookFra)
        MyBase.new()
        Me.InitializeComponent()
        mBookFra = oBookFra
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mBookFra
            Xl_Contact1.Contact = .Contact
            Xl_Cta1.Cta = .Cta
            TextBoxFranum.Text = .FraNum
            Xl_AmtBase.Amt = .Base
            Xl_AmtIVA.Amt = .Iva
            Xl_AmtIRPF.Amt = .Irpf
            Xl_DocFile1.Load(.Cca.DocFile)
            Calcula()
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Sub Calcula()
        Dim oTot As New maxisrvr.Amt
        Dim oBase As maxisrvr.Amt = Xl_AmtBase.Amt
        Dim oIVA As maxisrvr.Amt = Xl_AmtIVA.Amt
        Dim oIrpf As maxisrvr.Amt = Xl_AmtIRPF.Amt
        If oBase IsNot Nothing Then
            oTot = oBase.Clone
            oTot.Add(oIVA)
            oTot.Substract(oIrpf)
            Xl_AmtLiq.Amt = oTot

            If oBase.Eur = 0 Then
                LabelIVA.Text = ""
                LabelIrpf.Text = ""
            Else
                If oIVA IsNot Nothing Then
                    LabelIVA.Text = CInt(100 * oIVA.Eur / oBase.Eur).ToString & "%"
                End If
                If oIrpf IsNot Nothing Then
                    LabelIrpf.Text = CInt(100 * oIrpf.Eur / oBase.Eur).ToString & "%"
                End If
                End If
            End If

    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_Contact1.AfterUpdate, _
          Xl_Cta1.AfterUpdate, _
            TextBoxFranum.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub AmountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
             Xl_AmtBase.AfterUpdate, _
              Xl_AmtIVA.AfterUpdate, _
               Xl_AmtIRPF.AfterUpdate

        If mAllowEvents Then
            Calcula()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With mBookFra
            .Contact = Xl_Contact1.Contact
            .Cta = Xl_Cta1.Cta
            .FraNum = TextBoxFranum.Text
            .Base = Xl_AmtBase.Amt
            .Iva = Xl_AmtIVA.Amt
            .Irpf = Xl_AmtIRPF.Amt

            If BookFraLoader.Update(mBookFra, exs) Then
                If Xl_DocFile1.IsDirty Then
                    .Cca.DocFile = Xl_DocFile1.Value
                    If .Cca.Update( exs) Then
                        RaiseEvent AfterUpdate(mBookFra, System.EventArgs.Empty)
                        Me.Close()
                    Else
                        MsgBox("error al desar assentament de la factura" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                Else
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(mBookFra))
                    Me.Close()
                End If
            Else
                MsgBox("error al desar la factura:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If

        End With


    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If BookFraLoader.Delete(mBookFra, exs) Then
            Me.Close()
        Else
            MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

End Class