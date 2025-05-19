import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonaService } from '../persona.service';
import { Persona } from '../persona';

@Component({
  selector: 'app-persona-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './persona-form.component.html'
})
export class PersonaFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private personaService = inject(PersonaService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form = this.fb.group({
    id: [''],
    nombreCompleto: ['', Validators.required],
    identificacion: ['', Validators.required],
    edad: [0, [Validators.required, Validators.min(18), Validators.max(99)]],
    genero: ['', Validators.required],
    estaActiva: [true],
    atributosAdicionales: [''],
    maneja: [false],
    usaLentes: [false],
    diabetico: [false],
    padeceOtraEnfermedad: [false],
    cualOtraEnfermedad: ['']
  });

  isEdit = false;
  personaId?: string;

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEdit = true;
      this.personaId = idParam;
      this.personaService.getById(this.personaId).subscribe(p => {
        this.form.patchValue(p);
      });
    }

    // Mostrar/ocultar campo 'cualOtraEnfermedad'
    this.form.get('padeceOtraEnfermedad')?.valueChanges.subscribe(padece => {
      const cualCtrl = this.form.get('cualOtraEnfermedad');
      if (padece) {
        cualCtrl?.addValidators(Validators.required);
      } else {
        cualCtrl?.clearValidators();
      }
      cualCtrl?.updateValueAndValidity();
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const unaPersona: Persona = this.form.value;

    if (this.isEdit && this.personaId) {
      this.personaService.update(this.personaId, unaPersona).subscribe(() => {
        alert("Persona modificada exitosamente!")
        this.router.navigate(['/personas']);
      });
    } else {
      this.personaService.create(unaPersona).subscribe(() => {
        alert("Persona creada exitosamente!")
        this.router.navigate(['/personas']);
      });
    }
  }

  
cancelar() {
  this.router.navigate(['/personas']);
}

}
