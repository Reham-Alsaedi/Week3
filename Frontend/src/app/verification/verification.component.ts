import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DocumentService } from '../document.service';
import { ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-verification',
  standalone: true,
  imports: [ReactiveFormsModule], // Import ReactiveFormsModule here
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent {
  verificationForm: FormGroup;

  constructor(private fb: FormBuilder, private documentService: DocumentService) {
    this.verificationForm = this.fb.group({
      verificationCode: ['', [Validators.required]]
    });
  }

  onSubmit(): void {
    if (this.verificationForm.invalid) {
      return;
    }

    const verificationCode = this.verificationForm.value.verificationCode;

    this.documentService.verifyDocument(verificationCode).subscribe(response => {
      alert('Document verified successfully!');
    });
  }
}

