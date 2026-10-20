export default function (plop) {

  // =========================
  // 🔹 1. FEATURE GENERATOR
  // =========================
  plop.setGenerator('feature', {
    description: 'Crear nueva feature (Vertical Slice)',
    prompts: [
      {
        type: 'input',
        name: 'featureName',
        message: 'Nombre de la feature (ej: rooms, booking):'
      },
      {
        type: 'confirm',
        name: 'addPage',
        message: '¿Crear Page?',
        default: false
      }
    ],
    actions: function (data) {
      const actions = [];
      const basePath = 'src/features/{{kebabCase featureName}}';

      // --- Carpetas base ---
      actions.push({
        type: 'add',
        path: `${basePath}/components/.gitkeep`,
        template: ''
      });

      actions.push({
        type: 'add',
        path: `${basePath}/hooks/use{{pascalCase featureName}}.ts`,
        template: `import { useEffect, useState } from "react";
import { get{{pascalCase featureName}} } from "../services/{{camelCase featureName}}.service";

export function use{{pascalCase featureName}}() {
  const [data, setData] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    get{{pascalCase featureName}}().then((res) => {
      setData(res);
      setLoading(false);
    });
  }, []);

  return { data, loading };
}`
      });

      actions.push({
        type: 'add',
        path: `${basePath}/services/{{camelCase featureName}}.service.ts`,
        template: `import apiClient from "../../../shared/services/apiClient";
import type { {{pascalCase featureName}} } from "../types/{{camelCase featureName}}.types";

export async function get{{pascalCase featureName}}(): Promise<{{pascalCase featureName}}[]> {
  const response = await apiClient.get("/{{kebabCase featureName}}");
  return response.data;
}

export async function get{{pascalCase featureName}}ById(id: string): Promise<{{pascalCase featureName}}> {
  const response = await apiClient.get(\`/{{kebabCase featureName}}/\${id}\`);
  return response.data;
}
`
      });

      actions.push({
        type: 'add',
        path: `${basePath}/types/{{camelCase featureName}}.types.ts`,
        template: `export interface {{pascalCase featureName}} {
  id: string;
  name: string;
}`
      });

      // --- Componente base ---
      actions.push({
        type: 'add',
        path: `${basePath}/components/{{pascalCase featureName}}.tsx`,
        template: `import styles from "./{{pascalCase featureName}}.module.css";

interface Props {
  data?: any;
}

export const {{pascalCase featureName}} = ({ data }: Props) => {
  return (
    <div className={styles.container}>
      <h2>{{pascalCase featureName}}</h2>
    </div>
  );
};`
      });

      actions.push({
        type: 'add',
        path: `${basePath}/components/{{pascalCase featureName}}.module.css`,
        template: `.container {
  padding: 1rem;
}`
      });

      // --- Page opcional ---
      if (data.addPage) {
        actions.push({
          type: 'add',
          path: `${basePath}/{{pascalCase featureName}}Page.tsx`,
          template: `import { use{{pascalCase featureName}} } from "./hooks/use{{pascalCase featureName}}";
import { {{pascalCase featureName}} } from "./components/{{pascalCase featureName}}";

export const {{pascalCase featureName}}Page = () => {
  const { data, loading } = use{{pascalCase featureName}}();

  if (loading) return <p>Cargando...</p>;

  return (
    <main>
      <h1>{{pascalCase featureName}}</h1>
      <{{pascalCase featureName}} data={data} />
    </main>
  );
};`
        });
      }

      return actions;
    }
  });

  // =========================
  // 🔹 2. COMPONENTE EN FEATURE
  // =========================
  plop.setGenerator('feature-component', {
    description: 'Crear componente dentro de una feature',
    prompts: [
      {
        type: 'input',
        name: 'feature',
        message: 'Feature (ej: home, rooms):'
      },
      {
        type: 'input',
        name: 'name',
        message: 'Nombre del componente:'
      }
    ],
    actions: [
      {
        type: 'add',
        path: 'src/features/{{kebabCase feature}}/components/{{pascalCase name}}.tsx',
        template: `import styles from "./{{pascalCase name}}.module.css";

interface Props {}

export const {{pascalCase name}} = ({}: Props) => {
  return (
    <div className={styles.container}>
      {{pascalCase name}}
    </div>
  );
};`
      },
      {
        type: 'add',
        path: 'src/features/{{kebabCase feature}}/components/{{pascalCase name}}.module.css',
        template: `.container {
  display: block;
}`
      }
    ]
  });

  // =========================
  // 🔹 3. SHARED COMPONENT
  // =========================
  plop.setGenerator('shared-component', {
    description: 'Crear componente compartido (shared/ui)',
    prompts: [
      {
        type: 'input',
        name: 'name',
        message: 'Nombre del componente (ej: Button):'
      }
    ],
    actions: [
      {
        type: 'add',
        path: 'src/shared/ui/{{pascalCase name}}/{{pascalCase name}}.tsx',
        template: `import styles from "./{{pascalCase name}}.module.css";

interface Props {}

export const {{pascalCase name}} = ({}: Props) => {
  return (
    <div className={styles.base}>
      {{pascalCase name}}
    </div>
  );
};`
      },
      {
        type: 'add',
        path: 'src/shared/ui/{{pascalCase name}}/{{pascalCase name}}.module.css',
        template: `.base {
  border: 1px solid #ccc;
  padding: 0.5rem;
}`
      }
    ]
  });

};