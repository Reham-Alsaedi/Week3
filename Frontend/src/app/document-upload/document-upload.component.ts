import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DocumentService } from '../document.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-document-upload',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule], // ✅ Import CommonModule for Angular directives
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.css']
})
export class DocumentUploadComponent {
  uploadForm: FormGroup;
  isUploading = false; // ✅ Track upload state

  constructor(private fb: FormBuilder, private documentService: DocumentService) {
    this.uploadForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]], // ✅ Ensure title has at least 3 characters
      file: [null, Validators.required] // ✅ Use null instead of empty string for files
    });
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.uploadForm.patchValue({ file });
    }
  }

  onSubmit(): void {
    if (this.uploadForm.invalid) {
      return;
    }

    this.isUploading = true; // ✅ Show loading state

    const formData = new FormData();
    formData.append('title', this.uploadForm.value.title);
    formData.append('file', this.uploadForm.value.file);

    this.documentService.uploadDocument(formData).subscribe({
      next: () => {
        alert('Document uploaded successfully!');
        this.uploadForm.reset(); // ✅ Reset form after success
      },
      error: () => alert('Upload failed. Please try again.'),
      complete: () => (this.isUploading = false) // ✅ Reset loading state
    });
  }

  // ✅ Getters for easy template access
  get title() {
    return this.uploadForm.get('title');
  }
  get file() {
    return this.uploadForm.get('file');
  }
}
