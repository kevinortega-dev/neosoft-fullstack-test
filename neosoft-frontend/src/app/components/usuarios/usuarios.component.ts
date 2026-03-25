import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './usuarios.component.html',
  styleUrl: './usuarios.component.css'
})
export class UsuariosComponent implements OnInit {

  usuarios: any[] = [];
  roles: any[] = [];
  mostrarFormulario = false;
  editando = false;
  cargando = false;
  errorMsg = '';
  successMsg = '';

  usuarioForm = {
    id: 0,
    nombre: '',
    email: '',
    rolId: 0
  };

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargarUsuarios();
    this.cargarRoles();
  }

  cargarUsuarios() {
    this.cargando = true;
    this.api.getUsuarios().subscribe({
      next: (data) => {
        this.usuarios = data;
        this.cargando = false;
      },
      error: () => {
        this.errorMsg = 'Error al cargar los usuarios';
        this.cargando = false;
      }
    });
  }

  cargarRoles() {
    this.api.getRoles().subscribe({
      next: (data) => {
        this.roles = data;
      },
      error: () => {
        this.errorMsg = 'Error al cargar los roles';
      }
    });
  }

  abrirFormularioNuevo() {
    this.editando = false;
    this.usuarioForm = { id: 0, nombre: '', email: '', rolId: 0 };
    this.mostrarFormulario = true;
    this.errorMsg = '';
    this.successMsg = '';
  }

  editarUsuario(usuario: any) {
    this.editando = true;
    this.usuarioForm = {
      id: usuario.id,
      nombre: usuario.nombre,
      email: usuario.email,
      rolId: usuario.rolId
    };
    this.mostrarFormulario = true;
    this.errorMsg = '';
    this.successMsg = '';
  }

  guardarUsuario() {
    if (!this.usuarioForm.nombre.trim()) {
      this.errorMsg = 'El nombre es obligatorio';
      return;
    }
    if (!this.usuarioForm.email.trim()) {
      this.errorMsg = 'El email es obligatorio';
      return;
    }
    if (!this.usuarioForm.rolId || this.usuarioForm.rolId === 0) {
      this.errorMsg = 'Debe seleccionar un rol';
      return;
    }

    // validacion basica de email
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.usuarioForm.email)) {
      this.errorMsg = 'El email no tiene un formato válido';
      return;
    }

    const data = {
      nombre: this.usuarioForm.nombre,
      email: this.usuarioForm.email,
      rolId: Number(this.usuarioForm.rolId)
    };

    if (this.editando) {
      this.api.actualizarUsuario(this.usuarioForm.id, data).subscribe({
        next: () => {
          this.successMsg = 'Usuario actualizado correctamente';
          this.mostrarFormulario = false;
          this.cargarUsuarios();
        },
        error: (err) => {
          this.errorMsg = err.error?.mensaje || 'Error al actualizar el usuario';
        }
      });
    } else {
      this.api.crearUsuario(data).subscribe({
        next: () => {
          this.successMsg = 'Usuario creado correctamente';
          this.mostrarFormulario = false;
          this.cargarUsuarios();
        },
        error: (err) => {
          this.errorMsg = err.error?.mensaje || 'Error al crear el usuario';
        }
      });
    }
  }

  eliminarUsuario(id: number) {
    if (!confirm('¿Estás seguro que deseas eliminar este usuario?')) return;

    this.api.eliminarUsuario(id).subscribe({
      next: () => {
        this.successMsg = 'Usuario eliminado correctamente';
        this.cargarUsuarios();
      },
      error: (err) => {
        this.errorMsg = err.error?.mensaje || 'Error al eliminar el usuario';
      }
    });
  }

  cancelar() {
    this.mostrarFormulario = false;
    this.errorMsg = '';
    this.successMsg = '';
  }
}