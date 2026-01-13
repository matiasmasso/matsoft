

Public Class Xl_Fra_Menu
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
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Private mFras As MaxiSrvr.Fras

    Public WriteOnly Property Fras() As MaxiSrvr.Fras
        Set(ByVal Value As MaxiSrvr.Fras)
            mFras = Value
            With MyBase.MenuItems
                .Clear()

                .Add("Zoom", New System.EventHandler(AddressOf Zoom))
                .Add("Print", New System.EventHandler(AddressOf Print))

                'directori
                If mFras.count = 1 Then
                    'root.AddMenuContact(Me, mFras(0).Client, "client")
                End If
            End With
        End Set
    End Property

    Private Sub Zoom(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowFra(mFras(0))
    End Sub

    Private Sub Print(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PrintDoc
        With oFrm
            .ShowDialog()
            If Not .Cancel Then
                If .Preview Then
                    root.PrintFras(mFras)
                Else
                    If .Copia Then
                        root.PrintFras(mFras)
                    End If
                    If .Original Then
                        root.PrintFras(mFras)
                    End If
                End If
            End If
        End With
    End Sub


End Class