Public Class Xl_Alb_Menu
    Inherits System.Windows.Forms.ContextMenu

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Private mAlbs As MaxiSrvr.Albs

    Public WriteOnly Property Albs() As MaxiSrvr.Albs
        Set(ByVal Value As MaxiSrvr.Albs)
            Dim oItm As MenuItem
            mAlbs = Value
            With MyBase.MenuItems
                .Clear()
                oItm = .Add("Zoom", New System.EventHandler(AddressOf Zoom))
                oItm = .Add("Imprimir", New System.EventHandler(AddressOf Print))
                If mAlbs.Count > 1 Then oItm.Enabled = 0
                .Add("Facturar", New System.EventHandler(AddressOf Frx))
            End With
        End Set
    End Property

    Private Sub Zoom(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowAlb(mAlbs(0))
    End Sub

    Private Sub Frx(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ExeFacturacio(mAlbs)
    End Sub

    Private Sub Print(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PrintDoc
        With oFrm
            .ShowDialog()
            If Not .Cancel Then
                If .Preview Then
                    root.PrintAlbs(mAlbs)
                Else
                    If .Copia Then
                        root.PrintAlbs(mAlbs)
                    End If
                    If .Original Then
                        root.PrintAlbs(mAlbs)
                    End If
                End If
            End If
        End With
    End Sub
End Class


