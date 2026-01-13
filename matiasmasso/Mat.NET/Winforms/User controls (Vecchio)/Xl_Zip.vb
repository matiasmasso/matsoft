

Public Class Xl_Zip
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemZoom As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemClients As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.MenuItemZoom = New System.Windows.Forms.MenuItem()
        Me.MenuItemClients = New System.Windows.Forms.MenuItem()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.ContextMenu = Me.ContextMenu1
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(312, 38)
        Me.TextBox1.TabIndex = 1
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemZoom, Me.MenuItemClients})
        '
        'MenuItemZoom
        '
        Me.MenuItemZoom.Index = 0
        Me.MenuItemZoom.Text = "Zoom"
        '
        'MenuItemClients
        '
        Me.MenuItemClients.Index = 1
        Me.MenuItemClients.Text = "Clients"
        '
        'Xl_Zip
        '
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Xl_Zip"
        Me.Size = New System.Drawing.Size(312, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private _Zip As Zip
    Private _Country As DTOCountry

    Public Property Zip() As MaxiSrvr.Zip
        Get
            Return _Zip
        End Get
        Set(ByVal Value As Zip)
            _Zip = Value
            Display()
        End Set
    End Property

    Public WriteOnly Property Country() As DTOCountry
        Set(ByVal Value As DTOCountry)
            _Country = Value
        End Set
    End Property

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If Not _Zip Is Nothing Then
            Dim sPreviousText As String = _Zip.ZipyCityZon
            If TextBox1.Text = sPreviousText Then Exit Sub
        End If

        Dim oZips As Zips = Nothing
        If TextBox1.Text = "" Then
            oZips = New Zips
            _Zip = Nothing
        Else
            oZips = Zips.Search(TextBox1.Text, _Country)
            Select Case oZips.Count
                Case 0
                    Dim rc As MsgBoxResult
                    rc = MsgBox("Població no registrada." & vbCrLf & "La donem d'alta?", MsgBoxStyle.OkCancel, "M+O")
                    If rc = MsgBoxResult.Ok Then
                        Dim oFrm As New Frm_Geo(BLL.BLLApp.Org.Address.Zip.Location, Frm_Geo.SelectModes.SelectZip)
                        AddHandler oFrm.onItemSelected, AddressOf onNewZipCreated
                        oFrm.Show()
                        '_Zip = root.WzNewZip(TextBox1.Text, _Country)
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                Case 1
                    _Zip = oZips(0)
                Case Else
                    _Zip = SelectZipFromZips(oZips)
            End Select

        End If
        OnFormUpdate()
    End Sub

    Private Sub onNewZipCreated(Sender As Object, e As MatEventArgs)
        _Zip = e.Argument
        Display()
        RaiseEvent AfterUpdate(_Zip, EventArgs.Empty)
    End Sub



    Private Sub MenuItemZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemZoom.Click
        'Dim oFrm As New Frm_Zip_Old(_Zip)
        'AddHandler oFrm.AfterUpdate, AddressOf OnFormUpdate
        'oFrm.Show()
    End Sub

    Private Sub OnFormUpdate()
        If _Zip IsNot Nothing Then
            If _Zip.ZipyCityZon <> TextBox1.Text Then
                Display()
                RaiseEvent AfterUpdate(_Zip, EventArgs.Empty)
            End If
        End If
    End Sub

    Private Sub Display()
        If _Zip Is Nothing Then
            TextBox1.Clear()
        Else
            TextBox1.Text = _Zip.ZipyCityZon
        End If
    End Sub


End Class
