import { Component, OnInit } from '@angular/core';
import { DocumentService } from '../document.service'; // Create a service to interact with the backend
import { CommonModule } from '@angular/common'; // Import CommonModule

@Component({
  selector: 'app-dashboard',
  standalone: true,  // Make sure the component is standalone
  imports: [CommonModule], // Add CommonModule here
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  documents: any[] = [];  // Store documents

  constructor(private documentService: DocumentService) {}

  ngOnInit(): void {
    this.getDocuments();  // Fetch documents on component initialization
  }

  getDocuments(): void {
    this.documentService.getDocuments().subscribe((data) => {
      this.documents = data;
    });
  }
}

