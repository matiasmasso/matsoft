

Public Class Xl_Adr
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents Xl_CitPais1 As Xl_CitPais
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonGeo As System.Windows.Forms.Button

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Xl_Adr))
        Me.ButtonGeo = New System.Windows.Forms.Button
        Me.Xl_CitPais1 = New Xl_CitPais
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ButtonGeo
        '
        Me.ButtonGeo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGeo.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonGeo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonGeo.Image = CType(resources.GetObject("ButtonGeo.Image"), System.Drawing.Image)
        Me.ButtonGeo.Location = New System.Drawing.Point(258, 0)
        Me.ButtonGeo.Name = "ButtonGeo"
        Me.ButtonGeo.Size = New System.Drawing.Size(28, 40)
        Me.ButtonGeo.TabIndex = 0
        Me.ButtonGeo.UseVisualStyleBackColor = True
        '
        'Xl_CitPais1
        '
        Me.Xl_CitPais1.Zip = Nothing
        Me.Xl_CitPais1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_CitPais1.Location = New System.Drawing.Point(0, 20)
        Me.Xl_CitPais1.Name = "Xl_CitPais1"
        Me.Xl_CitPais1.Size = New System.Drawing.Size(258, 20)
        Me.Xl_CitPais1.TabIndex = 3
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.MaxLength = 60
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(258, 20)
        Me.TextBox1.TabIndex = 4
        '
        'Xl_Adr
        '
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Xl_CitPais1)
        Me.Controls.Add(Me.ButtonGeo)
        Me.Name = "Xl_Adr"
        Me.Size = New System.Drawing.Size(286, 40)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ChangedCit()
    Public Event ChangedPais(ByVal oCountry As Country)

    Private mAdr As MaxiSrvr.Adr
    Private mAllowEvents As Boolean

    Public Property Adr() As MaxiSrvr.Adr
        Get
            If mAdr Is Nothing Then mAdr = New MaxiSrvr.Adr
            With mAdr
                .Text = TextBox1.Text
                .Zip = Xl_CitPais1.Zip
            End With
            Return mAdr
        End Get
        Set(ByVal Value As MaxiSrvr.Adr)
            If Not Value Is Nothing Then
                mAdr = Value
                TextBox1.Text = mAdr.Text
                Xl_CitPais1.Zip = mAdr.Zip
                SetPinpoint()
                SetContextMenu()
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub SetPinpoint()
        Dim oCoordenadas As maxisrvr.GeoCoordenadas = mAdr.Coordenadas
        If oCoordenadas Is Nothing Then
            ButtonGeo.Enabled = False
            ButtonGeo.Image = My.Resources.pinpoint_empty
        Else
            ButtonGeo.Enabled = True
            Select Case oCoordenadas.Font
                Case maxisrvr.GeoCoordenadas.GeoFonts.Manual
                    ButtonGeo.Image = My.Resources.pinpoint_blue
                Case Else
                    ButtonGeo.Image = My.Resources.pinpoint_red
            End Select
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If mAllowEvents Then
            RaiseEvent Changed(Me, New EventArgs)
        End If
    End Sub

    Private Sub TextBox1_Validated(sender As Object, e As System.EventArgs) Handles TextBox1.Validated
        If mAdr Is Nothing Then mAdr = New MaxiSrvr.Adr

        If TextBox1.Text <> mAdr.Text Then
            mAdr.Text = TextBox1.Text
            If Xl_CitPais1.Zip IsNot Nothing Then
                SetPinpoint()
            End If
        End If
    End Sub

    Private Sub Xl_CitPais1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_CitPais1.AfterUpdate
        If mAllowEvents Then
            If mAdr Is Nothing Then mAdr = New MaxiSrvr.Adr
            mAdr.Zip = Xl_CitPais1.Zip
            'mAdr.Coordenadas = mAdr.GetCoordenadasFromGoogle
            SetPinpoint()
            RaiseEvent ChangedCit()
            RaiseEvent Changed(Me, New EventArgs)
        End If
    End Sub

    Private Sub ButtonGeo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGeo.Click
        Zoom()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem("google maps", Nothing, AddressOf Zoom)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("editar coordinades", Nothing, AddressOf Edit)
        oContextMenu.Items.Add(oMenuItem)

        ButtonGeo.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom()
        Dim sUrl As String = maxisrvr.Google.UrlFromCoordenadas(mAdr.Coordenadas)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Edit()
        Dim oFrm As New Frm_GoogleGeocode(mAdr.Coordenadas)
        AddHandler oFrm.AfterUpdate, AddressOf onCoordenadasUpdate
        oFrm.Show()
    End Sub

    Private Sub onCoordenadasUpdate(sender As Object, e As System.EventArgs)
        mAdr.Coordenadas = sender
        ButtonGeo.Image = IIf(mAdr.Coordenadas.Font = maxisrvr.GeoCoordenadas.GeoFonts.Manual, My.Resources.pinpoint_blue, My.Resources.pinpoint_red)
        SetPinpoint()
        RaiseEvent Changed(Me, System.EventArgs.Empty)
    End Sub
End Class
