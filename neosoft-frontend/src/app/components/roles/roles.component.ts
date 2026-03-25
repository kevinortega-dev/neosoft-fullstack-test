import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.css'
})
export class RolesComponent implements OnInit {

  roles: any[] = [];
  mostrarFormulario = false;
  editando = false;
  cargando = false;
  errorMsg = '';
  successMsg = '';

  rolForm = {
    id: 0,
    nombre: ''
  };

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargarRoles();
  }

  cargarRoles() {
    this.cargando = true;
    this.api.getRoles().subscribe({
      next: (data) => {
        this.roles = data;
        this.cargando = false;
      },
      error: () => {
        this.errorMsg = 'Error al cargar los roles';
        this.cargando = false;
      }
    });
  }

  abrirFormularioNuevo() {
    this.editando = false;
    this.rolForm = { id: 0, nombre: '' };
    this.mostrarFormulario = true;
    this.errorMsg = '';
    this.successMsg = '';
  }

  editarRol(rol: any) {
    this.editando = true;
    this.rolForm = { id: rol.id, nombre: rol.nombre };
    this.mostrarFormulario = true;
    this.errorMsg = '';
    this.successMsg = '';
  }

  guardarRol() {
    if (!this.rolForm.nombre.trim()) {
      this.errorMsg = 'El nombre del rol es obligatorio';
      return;
    }

    const data = { nombre: this.rolForm.nombre };

    if (this.editando) {
      this.api.actualizarRol(this.rolForm.id, data).subscribe({
        next: () => {
          this.successMsg = 'Rol actualizado correctamente';
          this.mostrarFormulario = false;
          this.cargarRoles();
        },
        error: (err) => {
          this.errorMsg = err.error?.mensaje || 'Error al actualizar el rol';
        }
      });
    } else {
      this.api.crearRol(data).subscribe({
        next: () => {
          this.successMsg = 'Rol creado correctamente';
          this.mostrarFormulario = false;
          this.cargarRoles();
        },
        error: (err) => {
          this.errorMsg = err.error?.mensaje || 'Error al crear el rol';
        }
      });
    }
  }

  eliminarRol(id: number) {
    if (!confirm('¿Estás seguro que deseas eliminar este rol?')) return;

    this.api.eliminarRol(id).subscribe({
      next: () => {
        this.successMsg = 'Rol eliminado correctamente';
        this.cargarRoles();
      },
      error: (err) => {
        this.errorMsg = err.error?.mensaje || 'Error al eliminar el rol';
      }
    });
  }

  cancelar() {
    this.mostrarFormulario = false;
    this.errorMsg = '';
    this.successMsg = '';
  }
}