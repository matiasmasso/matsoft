
Public Class Frm_Csa
    Private _Csa As DTOCsa

    Public Event AfterUpdate(sender As Object, sender As MatEventArgs)

    Public Sub New(value As DTOCsa)
        MyBase.New
        InitializeComponent()

        _Csa = value
    End Sub

    Private Sub Frm_Csa_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Csa IsNot Nothing Then
            If FEB2.Csa.Load(_Csa, exs) Then
                With _Csa
                    Me.Text = "Remesa de efectes num." & .Id
                    TextBoxBanc.Text = .Banc.Nom
                    DateTimePicker1.Value = .Fch
                    CheckBoxDescomptat.Checked = .Descomptat
                    Xl_Csbs1.Load(_Csa.Items)
                    LabelStatus.Text = String.Format("Total {0} efectes per {1:#,##0.00} €", _Csa.Items.Count, _Csa.Items.Sum(Function(x) x.Amt.Eur))
                End With
                Dim oMenuCsa As New Menu_Csa(_Csa)
                ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenuCsa.Range)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


    Private Async Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Retrocedim la remesa?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Csa.Delete(_Csa, exs) Then
                MsgBox("Remesa eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub



End Class