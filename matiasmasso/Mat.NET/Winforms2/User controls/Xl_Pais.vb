Imports System.ComponentModel


Public Class Xl_Pais
    Private _Country As DTOCountry

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Country() As DTOCountry
        Get
            Return _Country
        End Get
        Set(ByVal value As DTOCountry)
            _Country = value
            Refresca()
        End Set
    End Property

    <CategoryAttribute("Application"), _
       Browsable(True), _
       [ReadOnly](False), _
       BindableAttribute(False), _
       DefaultValueAttribute("True"), _
       DesignOnly(False), _
       DescriptionAttribute("decideix si mostra la bandera a la esquerra del codi Iso o no")> _
    Public Property FlagVisible() As Boolean
        Get
            Return PictureBox1.Visible
        End Get
        Set(ByVal value As Boolean)
            PictureBox1.Visible = value
            If value Then
                Me.Width = PictureBox1.Left + PictureBox1.Width
            Else
                Me.Width = Me.Width - PictureBox1.Width
            End If
        End Set
    End Property

    Private Sub Refresca()
        Dim sToolTipText As String = ""

        If _Country Is Nothing Then
            Label1.Text = ""
            sToolTipText = "doble clic per assignar un pais"
        Else
            Label1.Text = _Country.ISO
            sToolTipText = _Country.LangNom.Tradueix(Current.Session.User.Lang) & " (doble clic per canviar)"
        End If

        Dim toolTip1 As New ToolTip()

        ' Set up the delays for the ToolTip.
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        ' Force the ToolTip text to be displayed whether or not the form is active.
        toolTip1.ShowAlways = True

        ' Set up the ToolTip text for the Button and Checkbox.
        toolTip1.SetToolTip(Label1, sToolTipText)
        toolTip1.SetToolTip(PictureBox1, sToolTipText)
        toolTip1.SetToolTip(Me, sToolTipText)
        PictureBox1.AccessibleDescription = sToolTipText

        SetContextMenu()
    End Sub

    Private Sub Control_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Label1.DoubleClick, PictureBox1.DoubleClick, Me.DoubleClick
        Do_Change(sender, e)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Country IsNot Nothing Then
            Dim oMenu_Pais As New Menu_Country(_Country)
            AddHandler oMenu_Pais.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pais.Range)
            oContextMenu.Items.Add(MenuItem_Change)
        End If

        Me.ContextMenuStrip = oContextMenu
        Label1.ContextMenuStrip = oContextMenu
        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function MenuItem_Change() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Canviar"
        oMenuItem.Image = My.Resources.refresca
        AddHandler oMenuItem.Click, AddressOf Do_Change
        Return oMenuItem
    End Function

    Private Sub Do_Change(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Countries(_Country, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        _Country = e.Argument
        Refresca()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Country))
    End Sub
End Class
