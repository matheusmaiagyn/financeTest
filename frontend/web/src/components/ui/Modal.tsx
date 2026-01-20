import { useEffect } from "react";

type ModalProps = {
  open: boolean;
  title: string;
  onClose: () => void;
  children: React.ReactNode;
  zClassName?: string; 
};

export function Modal({ open, title, onClose, children, zClassName = "z-40" }: ModalProps) {
  useEffect(() => {
    if (!open) return;

    const onKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Escape") onClose();
    };

    window.addEventListener("keydown", onKeyDown);
    return () => window.removeEventListener("keydown", onKeyDown);
  }, [open, onClose]);

  if (!open) return null;

  return (
    <div className={`fixed inset-0 ${zClassName}`}>
      <div className="absolute inset-0 bg-black/60" onClick={onClose} />
      <div className="absolute inset-0 flex items-center justify-center p-4">
        <div
          className="w-full max-w-lg rounded-xl border border-slate-800 bg-slate-900 p-4 text-slate-50 shadow-lg"
          onClick={(e) => e.stopPropagation()}
        >
          <div className="mb-4 flex items-center justify-between">
            <h2 className="text-lg font-semibold">{title}</h2>
            <button
              type="button"
              onClick={onClose}
              className="rounded-md px-2 py-1 text-slate-300 hover:bg-slate-800 hover:text-white"
            >
              ✕
            </button>
          </div>

          {children}
        </div>
      </div>
    </div>
  );
}
