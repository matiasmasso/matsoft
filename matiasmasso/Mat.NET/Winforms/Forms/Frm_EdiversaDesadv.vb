Public Class Frm_EdiversaDesadv
    Private _Value As DTOEdiversaDesadv

    Public Sub New(value As DTOEdiversaDesadv)
        MyBase.New
        InitializeComponent()
        _Value = value
    End Sub

    Private Sub Frm_EdiversaDesadv_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim exs As New List(Of Exception)
        If FEB2.EdiversaDesadv.Load(exs, _Value) Then
            TextBoxDoc.Text = DTOEdiversaDesadv.GetFullDocNom(_Value)
            TextBoxProveidor.Text = DTOEdiversaDesadv.GetProveidorNom(_Value)
            TextBoxEntrega.Text = DTOEdiversaDesadv.GetEntregaNom(_Value)
            TextBoxFchShip.Text = DTOEdiversaDesadv.GetFchShip(_Value)
            TextBoxPdc.Text = DTOEdiversaDesadv.GetPdcText(_Value)
            Xl_EdiversaDesadvItems1.Load(_Value.Items)

            Dim oContextMenu As New ContextMenuStrip
            If _Value.PurchaseOrder IsNot Nothing Then
                Dim oMenu As New Menu_PurchaseOrder(_Value.PurchaseOrder)
                AddHandler oMenu.AfterUpdate, AddressOf refreshrequest
                oContextMenu.Items.AddRange(oMenu.Range)
            End If
            TextBoxPdc.ContextMenuStrip = oContextMenu
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub refreshrequest()
        refresca
    End Sub

    Private Sub TextBoxPdc_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxPdc.DoubleClick
        If _Value.PurchaseOrder IsNot Nothing Then
            Dim oFrm As New Frm_PurchaseOrder_Proveidor(_Value.PurchaseOrder)
            AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
            oFrm.Show()
        End If
    End Sub
End Class