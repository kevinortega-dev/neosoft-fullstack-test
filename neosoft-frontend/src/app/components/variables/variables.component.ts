import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-variables',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './variables.component.html',
  styleUrl: './variables.component.css'
})
export class VariablesComponent implements OnInit {

  variables: any[] = [];
  mostrarFormulario = false;
  editando = false;
  cargando = false;
  errorMsg = '';
  successMsg = '';

  variableForm = {
    id: 0,
    nombre: '',
    valor: '',
    tipo: ''
  };

  tiposDisponibles = ['texto', 'numérico', 'booleano'];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargarVariables();
  }

  cargarVariables() {
    this.cargando = true;
    this.api.getVariables().subscribe({
      next: (data) => {
        this.variables = data;
        this.cargando = false;
      },
      error: () => {
        this.errorMsg = 'Error al cargar las variables';
        this.cargando = false;
      }
    });
  }

  abrirFormularioNuevo() {
    this.editando = false;
    this.variableForm = { id: 0, nombre: '', valor: '', tipo: '' };
    this.mostrarFormulario = true;
    this.errorMsg = '';
    this.successMsg = '';
  }

  editarVariable(variable: any) {
    this.editando = true;
    this.variableForm = {
      id: variable.id,
      nombre: variable.nombre,
      valor: variable.valor,
      tipo: variable.tipo
    };
    this.mostrarFormulario = true;
    this.errorMsg = '';
    this.successMsg = '';
  }

  guardarVariable() {
    if (!this.variableForm.nombre.trim()) {
      this.errorMsg = 'El nombre es obligatorio';
      return;
    }
    if (!this.variableForm.valor.trim()) {
      this.errorMsg = 'El valor es obligatorio';
      return;
    }
    if (!this.variableForm.tipo) {
      this.errorMsg = 'Debe seleccionar un tipo';
      return;
    }

    const data = {
      nombre: this.variableForm.nombre,
      valor: this.variableForm.valor,
      tipo: this.variableForm.tipo
    };

    if (this.editando) {
      this.api.actualizarVariable(this.variableForm.id, data).subscribe({
        next: () => {
          this.successMsg = 'Variable actualizada correctamente';
          this.mostrarFormulario = false;
          this.cargarVariables();
        },
        error: (err) => {
          this.errorMsg = err.error?.mensaje || 'Error al actualizar la variable';
        }
      });
    } else {
      this.api.crearVariable(data).subscribe({
        next: () => {
          this.successMsg = 'Variable creada correctamente';
          this.mostrarFormulario = false;
          this.cargarVariables();
        },
        error: (err) => {
          this.errorMsg = err.error?.mensaje || 'Error al crear la variable';
        }
      });
    }
  }

  eliminarVariable(id: number) {
    if (!confirm('¿Estás seguro que deseas eliminar esta variable?')) return;

    this.api.eliminarVariable(id).subscribe({
      next: () => {
        this.successMsg = 'Variable eliminada correctamente';
        this.cargarVariables();
      },
      error: (err) => {
        this.errorMsg = err.error?.mensaje || 'Error al eliminar la variable';
      }
    });
  }

  cancelar() {
    this.mostrarFormulario = false;
    this.errorMsg = '';
    this.successMsg = '';
  }
}